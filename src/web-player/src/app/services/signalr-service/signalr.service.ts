import { Injectable, Output, EventEmitter } from '@angular/core';
import { ApiService } from '../api-service/api.service';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private hub: HubConnection;

  @Output() OnAddSong: EventEmitter<any> = new EventEmitter();
  @Output() OnRemoveSong: EventEmitter<any> = new EventEmitter();
  @Output() OnReaction: EventEmitter<any> = new EventEmitter();

  constructor(private api: ApiService) { }

  connect() {
    this.api.negotiate().subscribe((x) => this.connectSignalR(x));
  }

  private connectSignalR(auth: any) {
    this.hub = new HubConnectionBuilder().withUrl(auth.url, {
        accessTokenFactory: () => auth.accessToken,
    }).build();

    this.setUpEvents();

    return this.hub.start();
  }

  private setUpEvents() {
    this.hub.on('addSongCommandNotification', (data) =>  {
      this.OnAddSong.emit(data);
    });

    this.hub.on('removeSongCommandNotification', (data) =>  {
      this.OnRemoveSong.emit(data);
    });

    this.hub.on('reactionCommandNotification', (data) =>  {
      this.OnReaction.emit(data);
    });
  }
}
