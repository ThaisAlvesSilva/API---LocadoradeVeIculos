import { Injectable } from '@angular/core';
import { environment } from '../environment/environment.module';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Veiculo } from '../model/Veiculo';

@Injectable({
  providedIn: 'root'
})
export class VeiculoService {

  constructor(private http:HttpClient) { }

  getVeiculosDisponiveis():Observable<any>{
    const url = `${environment.baseUrlApi}/Veiculos/VeiculosDisponiveis`
    return this.http.get<Veiculo>(url, { responseType: 'json' });
  }

  atualizaVeiculo(id: number):Observable<any>{
    const url = `${environment.baseUrlApi}/Veiculos/atualizaStatus/${id}`;
    return this.http.put<Veiculo>(url, { responseType: 'json' });
  }

  atualizaVeiculoDisponivel(id: number):Observable<any>{
    const url = `${environment.baseUrlApi}/Veiculos/atualizaStatusDisponivel/${id}`;
    return this.http.put<Veiculo>(url, { responseType: 'json' });
  }
}

