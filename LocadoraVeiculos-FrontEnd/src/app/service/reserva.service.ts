import { Injectable } from '@angular/core';
import { Reserva } from '../model/Reserva';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment.module';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {

  constructor(private http:HttpClient) { }

  cadastrarReserva(novaReserva:Reserva):Observable<any>{
    return this.http.post<Reserva>(`${environment.baseUrlApi}/Reservas`, novaReserva);
  }

  atualizaReserva(id: number, reserva: Reserva):Observable<any>{
    const url = `${environment.baseUrlApi}/Reservas/cancelaReserva/${id}`;
    return this.http.put<Reserva>(url, { responseType: 'json' });
  }

  getReservas(id:number):Observable<any>{
    const url = `${environment.baseUrlApi}/Reservas/${id}/reservas`
    return this.http.get<Reserva>(url, { responseType: 'json' });
  }
}
