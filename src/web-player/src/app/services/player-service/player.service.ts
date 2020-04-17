import { Injectable, NgZone, Output, EventEmitter } from '@angular/core';
import { ApiService } from '../api-service/api.service';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  player: YT.Player;
  nowPlaying: any;

  @Output() songHasFinished: EventEmitter<any>;

  constructor(private zone: NgZone, private apiService: ApiService) {
    this.songHasFinished = new EventEmitter();
  }

  setupPlayer(player) {
    this.player = player;
  }

  changePlayerState(event) {
    const state = event.data;
    console.log(event);
    // let autoNext = false;
    // play the next song if its not the end of the playlist
    // should add a "repeat" feature
    if (state === YT.PlayerState.ENDED) {
      this.apiService.stopPlaying().subscribe((x) => console.log(x));
      this.nowPlaying = undefined;
      this.songHasFinished.emit();
    }

    if (state === YT.PlayerState.PAUSED) {
      // service.playerState = YT.PlayerState.PAUSED;
    }
    if (state === YT.PlayerState.PLAYING) {
      if (this.nowPlaying.url === 'http://www.youtube.com/watch?v=inPbTLwKa8w') {
        setTimeout(() => {
          this.player.stopVideo();
          this.nowPlaying = undefined;
          this.songHasFinished.emit();
        }, 185000);
      }
    }
  }

  setSize(height, width) {
    this.zone.runOutsideAngular(() => {
      this.player.setSize(width, height);
    });
  }

  playVideo(song: any, seconds?: number) {
    this.nowPlaying = song;
    const id = song.url.replace('http://www.youtube.com/watch?v=', '');
    const isLoaded = this.player.getVideoUrl().includes(id);
    if (!isLoaded) {
      this.zone.runOutsideAngular(() =>
        this.player.loadVideoById(id, seconds || undefined)
      );
    }
    this.play();
  }

  play() {
    this.zone.runOutsideAngular(() => this.player.playVideo());
  }
}
