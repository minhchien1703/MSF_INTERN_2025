import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SettingsComponent } from './layout/settings/settings.component';
import { RegisterComponent } from './auth/register/register.component';
import { ValidEmailComponent } from './auth/valid-email/valid-email.component';
import { AuthComponent } from './auth/auth.component';
import { LayoutComponent } from './layout/layout.component';
import { SystemDiaryComponent } from './layout/system-history/system-diary.component';
import { UserGroupsComponent } from './layout/user-groups/user-groups.component';
import { RoleGroupsComponent } from './layout/role-groups/role-groups.component';
import { DashboardComponent } from './layout/dashboard/dashboard.component';

export const routes: Routes = [
    { path: "", redirectTo: "auth/login", pathMatch: 'full' },
    {
        path: "auth", component: AuthComponent,
        children: [
            { path: "login", component: LoginComponent },
            { path: "register", component: RegisterComponent },
            { path: "valid-email", component: ValidEmailComponent },
        ]
    },
    {
        path: "layout",
        component: LayoutComponent,
        children: [
            { path: "dashboard", component: DashboardComponent },
            { path: "setting", component: SettingsComponent },
            { path: "system-diary", component: SystemDiaryComponent },
            { path: "users", component: UserGroupsComponent },
            { path: "roles", component: RoleGroupsComponent }
        ]
    }
];

