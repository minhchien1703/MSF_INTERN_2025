import { NgFor } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-system-diary',
  imports: [NgFor],
  templateUrl: './system-diary.component.html',
  styleUrl: './system-diary.component.css'
})
export class SystemDiaryComponent {
  histories = [
    {
      service: 'AuthService',
      methodName: 'authenticate',
      user: 'john_doe',
      ipAddress: '192.168.1.1',
      timeStamp: '2025-03-28 10:30:00',
      timeLimit: '30 minutes',
      browserInfo: 'Chrome 112.0.5615.137'
    },
    {
      service: 'UserService',
      methodName: 'getUserProfile',
      user: 'jane_smith',
      ipAddress: '192.168.1.2',
      timeStamp: '2025-03-28 11:00:00',
      timeLimit: '15 minutes',
      browserInfo: 'Firefox 98.0'
    },
    {
      service: 'AuthService',
      methodName: 'updatePassword',
      user: 'alice_wonder',
      ipAddress: '192.168.1.3',
      timeStamp: '2025-03-28 11:45:00',
      timeLimit: '5 minutes',
      browserInfo: 'Edge 100.0.1185.36'
    }
  ];
}
