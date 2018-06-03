import { VenueModel } from './../../classes/VenueModel';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  venues: VenueModel[] = [];

  constructor(public navCtrl: NavController, private httpClient: HttpClient) {
    this.httpClient.get<VenueModel[]>("http://localhost:5000/api/venues").subscribe(data => {
      data.forEach(venue => {
        this.venues.push(venue);
      });
    })
  }

}
