import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'viajes365-frontend';

  logout(): void {
    // ToDo
    // this.accountService.logout();
  }
}
