import { NgFor } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-role-groups',
  imports: [NgFor],
  templateUrl: './role-groups.component.html',
  styleUrl: './role-groups.component.css'
})
export class RoleGroupsComponent {
  roles = [
    {
      roleName: 'ADMIN',
      description: 'admin role',
      userCount: 2,
      createdAt: '2024-03-20',
      updatedAt: '2024-03-25'
    },
    {
      roleName: 'USER',
      description: 'user role',
      userCount: 9,
      createdAt: '2024-03-20',
      updatedAt: '2024-03-25'
    }
  ];
}
