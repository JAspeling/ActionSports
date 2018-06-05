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
    shownGroup = null;
    items = [];

    constructor(
        public navCtrl: NavController,
        public loadingCtrl: LoadingController,
        private venuesService: VenuesService
    ) {
        this.createLoading();

        this.items.push({isExpanded: false});

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
            content: 'Retrieving Venues...',
            spinner: 'dots'
        });
    }

    navigateToVenue(venue: VenueModel) {
        var navOptions = {
            animation: 'wd-transition'
        };
        this.navCtrl.push(LeaguePage, { venue: venue }, navOptions);
    }

    toggleGroup(group) {
        if (this.isGroupShown(group)) {
            this.shownGroup = null;
        } else {
            this.shownGroup = group;
        }
    }

    isGroupShown(group) {
        return this.shownGroup === group;
    }

    itemExpandHeight: number = 100;

    expandItem(standing) {
        this.items.map(listItem => {
            if (standing == listItem) {
                listItem.isExpanded = !listItem.isExpanded;
            } else {
                listItem.isExpanded = false;
            }
            return listItem;
        });
    }
}
