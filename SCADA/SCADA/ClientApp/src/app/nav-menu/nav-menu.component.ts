import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isLoggedIn = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  loggedIn() {
    const user = localStorage.getItem("user");
    this.isLoggedIn = !!user;
    return !!user;
  }

  admin() {
    return localStorage.getItem("user") === "ADMIN" && this.loggedIn();
  }

  logout() {
    localStorage.removeItem("user")
  }
}
