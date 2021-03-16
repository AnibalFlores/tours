import { Role } from './role';

export class User {
  userId!: number;
  title!: string;
  firstName!: string;
  lastName!: string;
  userName!: string;
  password!: string;
  email!: string;
  roleId!: number;
  role!: Role;
  isDeleting = false;
  token!: string;
  active!: boolean;
}
