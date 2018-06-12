import { Component } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SocialSharing } from '@ionic-native/social-sharing';
import { FabContainer, LoadingController, NavController, NavParams, ToastController } from 'ionic-angular';

import { MatchModel } from '../../classes/MatchModel';
import { ScoresheetService, MessageObject } from './../../services/scoresheetService';
import { BasePage } from './../base-page';

@Component({
    selector: 'page-scoresheet',
    templateUrl: 'scoresheet.html'
})
export class ScoresheetPage extends BasePage {
    scoresheetUrl: SafeUrl;
    match: MatchModel;

    constructor(
        public loadingController: LoadingController,
        public navParams: NavParams,
        public navController: NavController,
        public sanitizer: DomSanitizer,
        public toastCtrl: ToastController,
        private socialSharing: SocialSharing,
        private scoresheetService: ScoresheetService
    ) {
        super(loadingController, navParams, navController, toastCtrl);
        this.match = this.navParams.data.match;
        this.scoresheetUrl = this.sanitizer.bypassSecurityTrustResourceUrl('http://actionsport.spawtz.com' + this.match.scoreHref);
    }
    
    openSocial(network: string, fab: FabContainer) {
        this.createLoading('Converting scoresheet to PDF...');
        this.loader.present().then(() => {
            this.scoresheetService.convertToPdf(this.match).then((base64) => {
                this.scoresheetService.addToCache(this.match.score, base64);
                this.scoresheetService.saveBase64(base64, `${this.match.teamA} vs ${this.match.teamB} - ${this.match.score}`, this.match)
                    .then((messageObject: MessageObject) => {
                            this.socialSharing.shareViaWhatsApp(messageObject.message, messageObject.fileUrl).then(() => {
                            this.loader.dismiss();
                        }).catch(err => {
                            this.presentToast('Failed to send file via whatsapp');
                        });
                    }, err => {
                        this.presentToast('Failed to save file to Device');
                        this.loader.dismiss();
                    });
            }, err => {
                this.loader.dismiss();
                this.presentToast('Failed to convert file to PDF');
            });
        })
      }
}
