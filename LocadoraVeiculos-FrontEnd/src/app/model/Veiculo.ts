import { Reserva } from "./Reserva";
import { StatusVeiculo } from "./StatusVeiculo";

export class Veiculo{
    id: number = 0;
    placa: string = '';
    modelo: string = '';
    marca: string = '';
    status?: StatusVeiculo; 
    valorAluguelDia: number = 0;
    quilometragem: number = 0;
    reserva: Reserva = new Reserva();
}