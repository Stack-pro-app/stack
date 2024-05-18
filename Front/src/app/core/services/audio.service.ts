// audio.service.ts
import { Injectable } from '@angular/core';
import { Howl } from 'howler';

@Injectable({
  providedIn: 'root',
})
export class AudioService {
  private sound: Howl | null = null;
  public duration: number = 0;
  public currentTime: number = 0;

  constructor() {}

  load(src: string) {
    if (this.sound) {
      this.sound.unload();
    }
    this.sound = new Howl({
      src: [src],
      html5: true,
      onload: () => {
        this.duration = this.sound!.duration();
      },
      onplay: () => {
        this.updateProgress();
      },
    });
  }

  play() {
    this.sound?.play();
  }

  pause() {
    this.sound?.pause();
  }

  stop() {
    this.sound?.stop();
  }

  setVolume(volume: number) {
    this.sound?.volume(volume);
  }

  seek(position: number) {
    this.sound?.seek(position);
  }

  isPlaying(): boolean {
    return this.sound?.playing() || false;
  }

  private updateProgress() {
    setInterval(() => {
      if (this.sound?.playing()) {
        this.currentTime = this.sound.seek() as number;
      }
    }, 1000);
  }
}
