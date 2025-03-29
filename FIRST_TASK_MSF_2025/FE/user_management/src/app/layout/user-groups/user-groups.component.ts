import { NgFor } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-user-groups',
  imports: [NgFor],
  templateUrl: './user-groups.component.html',
  styleUrls: ['./user-groups.component.css']
})
export class UserGroupsComponent {
  users = [
    {
      account: 'user1',
      email: 'user1@example.com',
      roles: 'Admin',
      phone: '123-456-7890',
      firstName: 'John',
      lastName: 'Doe',
      status: 'Active',
      blockAccount: 'No',
      loginStatus: 'Online',
      createdAt: '2024-03-20',
      updatedAt: '2024-03-25'
    },
    {
      account: 'user2',
      email: 'user2@example.com',
      roles: 'User',
      phone: '987-654-3210',
      firstName: 'Jane',
      lastName: 'Smith',
      status: 'Inactive',
      blockAccount: 'Yes',
      loginStatus: 'Offline',
      createdAt: '2024-03-18',
      updatedAt: '2024-03-22'
    }
  ];
  
}
