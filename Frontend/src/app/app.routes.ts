import { Routes } from '@angular/router';
import { MainpageComponent } from './pages/mainpage/mainpage.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
  // Tự động chuyển hướng sang /main khi vào trang gốc
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  
  { path: 'main', component: MainpageComponent },
  { path: 'login', component: LoginComponent }
];