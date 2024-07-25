import { Injectable } from '@angular/core';
import { Cliente } from '../model/Cliente';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.module';
import { Endereco } from '../model/Endereco';
import { Reserva } from '../model/Reserva';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

   constructor(private http:HttpClient) { }
 
    //Método para cadastrar novo Cliente
    cadastrar(novoCliente:Cliente):Observable<any>{
      return this.http.post<Cliente>(`${environment.baseUrlApi}/Clientes`, novoCliente);
    }

    cadastrarEndereco(endereco:Endereco):Observable<any>{
      return this.http.post<Endereco>(`${environment.baseUrlApi}/Enderecos`, endereco);
    }

    getCep(cep:string):Observable<any>{
      return this.http.get<any>(`https://viacep.com.br/ws/${cep}/json/`, );
    }
 
   getUsuarioLogin(email:String, senha:String):Observable<any>{
     const url = `${environment.baseUrlApi}/Clientes/login/${email}/${senha}`
     return this.http.get<Cliente>(url, { responseType: 'json' });
   }

   

   
}
