import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,  RouterLink, HttpClientModule,],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'LocadoraVeiculos-FrontEnd';

  constructor(private router: Router){}

  ngOnInit(): void{
    const cliente = this.getObjetoLocalStorage();
    if(cliente){
      if(!cliente){
        this.router.navigate(['/Login']);
      }else{
        this.router.navigate(['/HomeCliente']);
      }
    }
    
    
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
