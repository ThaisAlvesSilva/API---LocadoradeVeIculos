import { Component } from '@angular/core';
import { ClienteService } from '../../service/cliente.service';
import { Reserva } from '../../model/Reserva';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ReservaService } from '../../service/reserva.service';
import { VeiculoService } from '../../service/veiculo.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home-cliente',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, ReactiveFormsModule, RouterLink],
  templateUrl: './home-cliente.component.html',
  styleUrl: './home-cliente.component.css'
})
export class HomeClienteComponent {
  reservas: Reserva[] = [];
  constructor(private serviceCliente:ClienteService, private router: Router, private serviceReservas: ReservaService, private serviceVeiculo:VeiculoService){}

  ngOnInit(): void{
    this.getReservas();
  }

  getReservas(){
    const usuarioLogado = this.getObjetoLocalStorage() || { id: ''};
    this.serviceReservas.getReservas(usuarioLogado.id)
      .subscribe(
        (retorno) => {
          this.reservas = retorno.data;
          console.log(this.reservas);
        },
        (error) => {
          console.error('Erro:', error);
        }
      );
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

  removeClienteLocalStorage(): void {
    localStorage.removeItem("Cliente");
  }

  atualizaStatusReserva(reserva:Reserva){
    if (reserva.id !== undefined) { 
      this.serviceReservas.atualizaReserva(reserva.id, reserva)
        .subscribe(
          (retorno) => {
            this.reservas = retorno.data;
            this.serviceVeiculo.atualizaVeiculoDisponivel(reserva.veiculoID)
            .subscribe(
              (retorno) => {
                
              });
          });
    }
  }

  logOut(){
    Swal.fire({
      title: "Tem certeza que deseja sair?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      cancelButtonText: "Cancelar",
      confirmButtonText: "Confirmar"
    }).then((result) => {
      if (result.isConfirmed) {
        this.removeClienteLocalStorage();
        this.router.navigate(['']);
      }
    });

  }
}
