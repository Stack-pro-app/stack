import { Component } from '@angular/core';

@Component({
  selector: 'app-theme-switcher',
  standalone: true,
  imports: [],
  templateUrl: './theme-switcher.component.html',
  styleUrl: './theme-switcher.component.css'
})
export class ThemeSwitcherComponent {
value = 'synthwave';
setTheme(theme:string) {
  localStorage.setItem('theme', theme);
}
}
