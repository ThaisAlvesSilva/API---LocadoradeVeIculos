import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { CadastroComponent } from './components/cadastro/cadastro.component';
import { HomeClienteComponent } from './components/home-cliente/home-cliente.component';
import { ReservaComponent } from './components/reserva/reserva.component';
import { HomePrincipalComponent } from './components/home-principal/home-principal.component';

export const routes: Routes = [
    // {path: "cadastro", component: CadastroUsuarioComponent},
    {
        path: '', 
        component: HomePrincipalComponent
    },
    {
        path: 'Login', 
        component: LoginComponent
    },
    {
        path: 'Cadastro', 
        component: CadastroComponent
    },
    {
        path: 'HomeCliente', 
        component: HomeClienteComponent
    },
    {
        path: 'Reservar', 
        component: ReservaComponent
    }

];
  
