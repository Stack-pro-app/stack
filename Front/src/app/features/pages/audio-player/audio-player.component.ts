import { CommonModule } from '@angular/common';
import { AudioService } from './../../../core/services/audio.service';
// audio-player.component.ts
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-audio-player',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './audio-player.component.html',
  styleUrl: './audio-player.component.css',
})
export class AudioPlayerComponent implements OnInit {
  @Input() audioUrl: string = '';

  constructor(public audioService: AudioService) {}

  ngOnInit() {
    this.loadAudio();
  }

  loadAudio() {
    this.audioService.load(this.audioUrl);
  }

  togglePlay() {
    if (this.audioService.isPlaying()) {
      this.audioService.pause();
    } else {
      this.loadAudio();
      this.audioService.play();
    }
  }

  setVolume(event: any) {
    const volume = event.target.value;
    this.audioService.setVolume(volume);
  }

  seek(event: any) {
    const position = event.target.value;
    this.audioService.seek(position);
  }

  formatTime(seconds: number): string {
    const minutes: number = Math.floor(seconds / 60);
    const secs: number = Math.floor(seconds % 60);
    return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
  }
}

