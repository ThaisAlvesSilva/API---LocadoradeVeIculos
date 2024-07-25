import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-home-principal',
  standalone: true,
  imports: [],
  templateUrl: './home-principal.component.html',
  styleUrl: './home-principal.component.css'
})
export class HomePrincipalComponent {


  constructor(private router: Router){}

  redirecionaUsuario(){
    this.router.navigate(['/Login']);
  }
}
