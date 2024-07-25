import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink} from '@angular/router';
import { ClienteService } from '../../service/cliente.service';
import { Cliente } from '../../model/Cliente';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm!: FormGroup;
  sucessLogin:boolean = true;

  constructor(private formBuilder: FormBuilder, private router: Router, private service:ClienteService ){}

  ngOnInit(): void{
    const cliente = this.getObjetoLocalStorage();
    if(!cliente){
      this.loginForm = this.formBuilder.group({
        senha: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]]
      })
      //console.log(cliente.id);
    }else{
      this.router.navigate(['/HomeCliente']);
    }
    
  }

  submit(){
    if(this.loginForm.valid){
      const {value} = this.loginForm;
      this.service.getUsuarioLogin(value.email, value.senha)
        .subscribe(
          (retorno) => {
            this.sucessLogin = true;
            this.adicionaLocalStorage(retorno.data);
            this.router.navigate(['/HomeCliente']);
          },
          (error) => {
            this.sucessLogin = false;
          }
        );
    }else{
      return;
    }
  }

  adicionaLocalStorage(cliente:Cliente){
    const clienteSerializado = JSON.stringify(cliente);
    localStorage.setItem('Cliente', clienteSerializado);
  }

  getObjetoLocalStorage(): any | null {
    if (typeof localStorage !== 'undefined') {
      const objetoSerializado = localStorage.getItem("Cliente");
      if (objetoSerializado) {
        return JSON.parse(objetoSerializado);
      }
    }
    
    return null
  }

  

}