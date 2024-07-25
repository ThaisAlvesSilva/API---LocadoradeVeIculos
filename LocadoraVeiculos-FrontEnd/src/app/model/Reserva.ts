import { Cliente } from "./Cliente";
import { StatusReserva } from "./StatusReserva";
import { Veiculo } from "./Veiculo";

export class Reserva{
    id: number = 0;
    dtInicio: Date = new Date();
    dtFim: Date = new Date();
    valor: number = 0;
    status?: StatusReserva; 
    ClienteID: number = 0;
    cliente?: Cliente;
    veiculoID: number = 0;
    veiculo?: Veiculo;
}