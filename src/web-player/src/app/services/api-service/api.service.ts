import { Injectable } from '@angular/core';
import { HttpClient  } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiEndpoint = 'https://evidences-api.azurewebsites.net/';
  private playerId = '8AC6F07F-62AE-4D86-AEF7-0299FF245BE2';

  constructor(private http: HttpClient) { }

  negotiate() {
    const request = {
      userId: this.playerId
    };

    return this.http.post(`${this.apiEndpoint}/v1/negotiate`, request);
  }

  startPlaying(songId) {
    const request = {
      songId
    };

    return this.http.post(`${this.apiEndpoint}/v1/song/current/start`, request);
  }

  stopPlaying() {
    return this.http.post(`${this.apiEndpoint}/v1/song/current/finish`, {});
  }
}
