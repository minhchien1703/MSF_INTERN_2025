import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BackgroudLeftAuthComponent } from './backgroud-left-auth/backgroud-left-auth.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth',
  imports: [RouterModule, BackgroudLeftAuthComponent],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {

}
