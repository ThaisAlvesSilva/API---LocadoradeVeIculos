import { CommonModule} from '@angular/common';
import { HttpClient, HttpClientModule} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl,FormGroup, ReactiveFormsModule,Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ClienteService } from '../../service/cliente.service';
import { Endereco } from '../../model/Endereco';
import { Cliente } from '../../model/Cliente';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink,HttpClientModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.css'
})
export class CadastroComponent {
  cadastroClienteForm!: FormGroup;
  self = this;
  constructor(private fb: FormBuilder, private service:ClienteService,  private router: Router,){}

  ngOnInit(): void{
    this.cadastroClienteForm = this.fb.group({
      nome: ['', [Validators.required]],
      telefone: ['', [Validators.required, Validators.pattern('^[0-9]{10,11}$')]],
      CPF: ['', [Validators.required, Validators.pattern('[0-9]{3}[0-9]{3}[0-9]{3}[0-9]{2}')]],
      CEP: ['', [Validators.required, Validators.pattern('^[0-9]{5}[0-9]{3}$')]],
      Rua: ['', [Validators.required]],
      Numero: ['', [Validators.required]],
      Bairro: ['', [Validators.required]],
      Cidade: ['', [Validators.required]],
      Estado: ['', [Validators.required]],
      senha: ['', [Validators.required]], // Alterei para um mínimo de 6 caracteres, ajuste conforme necessário
      email: ['', [Validators.required, Validators.email]]
    })
  }

  submit(){
    if(this.cadastroClienteForm.valid){
      const {value} = this.cadastroClienteForm;      //desestruturação do objeto: vai pegar a propriedade 'value' do form e atribuir 
    
      this.service.cadastrar(value)
        .subscribe(retorno => { 
          const endereco: Endereco = {
            cep: value.CEP,
            rua: value.Rua,
            numero: value.Numero,
            cidade: value.Cidade,
            bairro: value.Bairro,
            UF: value.Estado,
            ClienteID: retorno.data.id
          };
          
          this.adicionaLocalStorage(retorno.data);

          this.service.cadastrarEndereco(endereco)
            .subscribe(retornoEndereco => { 
              this.router.navigate(['/HomeCliente']);
          });

            //Limpando o formulário
            this.ngOnInit();
          });
        
    }else{
      return;
    }
  }

  adicionaLocalStorage(cliente:Cliente){
    const clienteSerializado = JSON.stringify(cliente);
    localStorage.setItem('Cliente', clienteSerializado);
  }

  pesquisaCep() {
    const cep = this.cadastroClienteForm.get('CEP')?.value;
    if (!this.cadastroClienteForm.controls['CEP'].hasError('pattern')) {
      this.service.getCep(cep).subscribe(retorno => { 
        this.cadastroClienteForm.patchValue({
          Rua: retorno.logradouro,
          Bairro: retorno.bairro,
          Cidade: retorno.localidade,
          Estado: retorno.uf
        });
      });

    }else{
      this.cadastroClienteForm.patchValue({
        Rua: '',
        Bairro: '',
        Cidade: '',
        Estado: ''
      });
    }
  }
}
