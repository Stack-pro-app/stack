import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../../core/services/store/store.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  themes:any[]= [
      "light",
      "dark",
      "cupcake",
      "bumblebee",
      "emerald",
      "corporate",
      "synthwave",
      "retro",
      "cyberpunk",
      "valentine",
      "halloween",
      "garden",
      "forest",
      "aqua",
      "lofi",
      "pastel",
      "fantasy",
      "wireframe",
      "black",
      "luxury",
      "dracula",
      "cmyk",
      "autumn",
      "business",
      "acid",
      "lemonade",
      "night",
      "coffee",
      "winter",
      "dim",
      "nord",
      "sunset",
    ];
  user: any = {
    name: 'Hayaoui Mouad',
    location: 'CasaBlanca',
    role: 'Admin',
    University: 'Abdelmalk Essaidi',
    Description:
      'A computer science student is an individual immersed in the study of computer systems, algorithms, programming languages, and the theoretical foundations of computing. Passionate about solving complex problems, this student learns to design and implement software, analyze data structures, and understand the intricacies of hardware and software interactions. They often engage in coding projects, collaborate with peers on innovative solutions, and stay abreast of the latest advancements in technology. With a curiosity for innovation and a love for logical reasoning, a computer science student is on a journey to acquire the skills and knowledge needed to navigate the ever-evolving landscape of computing.',
    Status: 'Online',
  };
  constructor(private store: StoreService) {}
  ngOnInit(): void {
    const decoded = this.store.getUser();
    console.log(decoded);
    this.user.name = decoded.name;
    this.user.role = decoded.role;
  }
}
