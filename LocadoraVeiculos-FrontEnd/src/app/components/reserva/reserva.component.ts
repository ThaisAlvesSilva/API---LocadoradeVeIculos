import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Veiculo } from '../../model/Veiculo';
import { VeiculoService } from '../../service/veiculo.service';
import { CommonModule, Location} from '@angular/common';
import { ReservaService } from '../../service/reserva.service';

@Component({
  selector: 'app-reserva',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reserva.component.html',
  styleUrl: './reserva.component.css'
})
export class ReservaComponent {
  veiculos: Veiculo[] = [];
  valorReserva: number = 0;
  minDate: string = '';
  minEndDate: string= '';

  dadosReservaForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router, private location: Location, private serviceVeiculo: VeiculoService, private serviceReserva: ReservaService ){}

  ngOnInit(): void{

    this.getVeiculos();

    //Definindo que a data inicial deve ser maior que a data atual
    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2); 
    const day = ('0' + today.getDate()).slice(-2); 
    this.minDate = `${year}-${month}-${day}`;
    this.minEndDate = this.minDate;

    this.dadosReservaForm = this.formBuilder.group({
      dtInicio   : ['', [Validators.required]],
      dtFim      : ['', Validators.required],
      status     : [0],
      VeiculoID  : ['', Validators.required],
      valorAluguelVeiculo  : ['']
    })

    //definindo que a data de fim deve ser maior ou igual a data inicial
    this.dadosReservaForm.get('dtInicio')!.valueChanges.subscribe((selectedDate) => {
      this.minEndDate = selectedDate;
      this.dadosReservaForm.get('dtFim')!.updateValueAndValidity();
    });
  }

  getVeiculos(){
    this.serviceVeiculo.getVeiculosDisponiveis()
      .subscribe(
        (retorno) => {
          this.veiculos = retorno.data;
          console.log(this.veiculos);
        }
      );
  }

  submit(){
    if(this.dadosReservaForm.valid){
      const {value} = this.dadosReservaForm;
      const usuario = this.getObjetoLocalStorage();
      value.ClienteID = usuario.id;

      const date1: Date = new Date(value.dtInicio);
      const date2: Date = new Date(value.dtFim);
      const diff = Math.abs(date1.getTime() - date2.getTime());
      const diffDays = Math.ceil(diff / (1000 * 3600 * 24));
      value.valor = value.valorAluguelVeiculo * diffDays;

      const veiculo: Veiculo | undefined = this.veiculos.find(v => v.id == value.VeiculoID) as Veiculo;
      
      value.valor = veiculo.valorAluguelDia * diffDays;

      this.serviceReserva.cadastrarReserva(value)
      .subscribe(
        (retorno) => {
          this.serviceVeiculo.atualizaVeiculo(value.VeiculoID)
          .subscribe(
            (retorno) => {
              this.router.navigate(['/HomeCliente']);
            });
          
        }
      );
    }else{
      return;
    }
  }

  goBack(): void {
    this.location.back();
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
