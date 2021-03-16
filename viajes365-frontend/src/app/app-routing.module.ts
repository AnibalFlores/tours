import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);

const routes: Routes = [
  { path: '', component: HomeComponent },

  { path: 'register', redirectTo: 'account/register', pathMatch: 'full' },
  { path: 'account', loadChildren: accountModule, pathMatch: 'prefix' },
  // cualquier otra cosa va a home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
