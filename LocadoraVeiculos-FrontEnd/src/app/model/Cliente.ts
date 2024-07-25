import {Endereco} from './Endereco';
import { Reserva } from './Reserva';
export class Cliente{
    Id?:number = 0;
    nome:string = '';
    telefone:string = '';
    cpf:string= '';
    email:string = '';
    senha:string= '';
    endereco: Endereco = new Endereco;
    reservas: Reserva[] = []; // Lista de reservas
}