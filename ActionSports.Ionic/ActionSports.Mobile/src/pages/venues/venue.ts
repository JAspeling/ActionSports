import { VenueModel } from './../../classes/VenueModel';
import { Component } from '@angular/core';
import { NavController, LoadingController, Loading } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';


@Component({
    selector: 'page-venue',
    templateUrl: 'venue.html'
})
export class VenuePage {

    venues: VenueModel[] = [];
    loader: Loading;

    constructor(public navCtrl: NavController,
        public loadingCtrl: LoadingController,
        private httpClient: HttpClient) {
        this.createLoading();

        this.loader.present();
        this.httpClient.get<VenueModel[]>("http://localhost:5000/api/venues").subscribe((data: VenueModel[]) => {
            data.forEach(venue => {
                this.venues.push(venue);
            });
            this.loader.dismiss();
        });
    }

    private getVenues(): Promise<VenueModel[]> {

        return this.httpClient.get<VenueModel[]>("http://localhost:5000/api/venues").toPromise();
    }

    createLoading() {
        this.loader = this.loadingCtrl.create({
            content: "Retrieving Venues...",
            spinner: "dots"
        });
    }

    navigateToVenue(venue: VenueModel) {
        debugger;
    }
}
