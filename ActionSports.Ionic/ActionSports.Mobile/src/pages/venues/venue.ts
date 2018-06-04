import { Component } from '@angular/core';
import { LeaguePage } from '../leagues/leagues';
import { Loading, LoadingController, NavController } from 'ionic-angular';
import { VenueModel } from './../../classes/VenueModel';
import { VenuesService } from '../../services/venuesService';

@Component({
    selector: 'page-venue',
    templateUrl: 'venue.html'
})
export class VenuePage {

    venues: VenueModel[] = [];
    loader: Loading;

    constructor(public navCtrl: NavController,
        public loadingCtrl: LoadingController,
        private venuesService: VenuesService
    ) {
        this.createLoading();

        this.loader.present().then(() => {
            this.venuesService.getVenues().then((venues: VenueModel[]) => {
                venues.forEach(venue => {
                    this.venues.push(venue);
                });
                this.loader.dismiss();
            });
        });
    }

    createLoading() {
        this.loader = this.loadingCtrl.create({
            content: "Retrieving Venues...",
            spinner: "dots"
        });
    }

    navigateToVenue(venue: VenueModel) {
        var navOptions = {
            animation: 'wd-transition'
       };
        this.navCtrl.push(LeaguePage, { venue: venue }, navOptions);
    }
}
