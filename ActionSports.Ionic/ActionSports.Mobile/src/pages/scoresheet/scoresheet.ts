import { Component } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SocialSharing } from '@ionic-native/social-sharing';
import { FabContainer, LoadingController, NavController, NavParams, ToastController } from 'ionic-angular';

import { MatchModel } from '../../classes/MatchModel';
import { ScoresheetService } from './../../services/scoresheetService';
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
        this.createLoading('Fetching scoresheet...');
        this.match = this.navParams.data.match;
        this.scoresheetUrl = this.sanitizer.bypassSecurityTrustResourceUrl('http://actionsport.spawtz.com' + this.match.scoreHref);
    }

    b64toBlob(b64Data, contentType, sliceSize) {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;
      
        var byteCharacters = atob(b64Data);
        var byteArrays = [];
      
        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
          var slice = byteCharacters.slice(offset, offset + sliceSize);
      
          var byteNumbers = new Array(slice.length);
          for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
          }
      
          var byteArray = new Uint8Array(byteNumbers);
      
          byteArrays.push(byteArray);
        }
      
        var blob = new Blob(byteArrays, {type: contentType});
        return blob;
      }
    
    openSocial(network: string, fab: FabContainer) {
        console.log('Share in ' + network); 
        const message = `${this.match.teamA} vs ${this.match.teamB} - ${this.match.score}`;
        const url = 'http://actionsport.spawtz.com' + this.match.scoreHref;


        
        
        this.scoresheetService.convertToPdf2(this.match);//.then((base64) => {

        //     var blob = this.b64toBlob(base64, 'application/pdf', 512);
        //     var blobUrl = URL.createObjectURL(blob);
            
        //     const fileUrl = `data:application/pdf;base64,${base64}`;
        //     this.socialSharing.shareViaWhatsApp(`${this.match.teamA} vs ${this.match.teamB} - ${this.match.score}`, blobUrl, 'http://actionsport.spawtz.com' + this.match.scoreHref);
        //     fab.close();
        // }, err => {
        //     console.log('Failed to retrieve PDF from the server.', err);
        //     fab.close();
        // });

        // this.socialSharing.canShareVia('whatsapp', message, null, null, url).then(() => { 
            
        //     this.scoresheetService.convertToPdf(this.match).then((base64) => {
        //         //this.socialSharing.shareViaWhatsApp(`${this.match.teamA} vs ${this.match.teamB} - ${this.match.score}`, '', 'http://actionsport.spawtz.com' + this.match.scoreHref);
        //         fab.close();
        //     }, err => {
        //         console.log('Failed to retrieve PDF from the server.', err);
        //         fab.close();
        //     });


        // }, err => {
        //     this.presentToast('Whatsapp not installed on device!');
        // });
      }
}
