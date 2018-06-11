import { MatchModel } from './../classes/MatchModel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppState } from '../classes/AppState';
import { File } from '@ionic-native/file';
import { SocialSharing } from '@ionic-native/social-sharing';

@Injectable()
export class ScoresheetService {
    constructor(
        private httpClient: HttpClient,
        private file: File,
        private socialSharing: SocialSharing
    ) {}

    convertToPdf(match: MatchModel) {
        const clone = new MatchModel(match);
        clone.scoreHref = AppState.baseUrl + match.scoreHref;
        return this.httpClient
            .post<string>(`${AppState.basePage}/scoresheet`, clone)
            .timeout(30000)
            .toPromise();
        // .then(base64 => {
        //     this.shareImg(clone, base64);
        // });
    }

    convertToPdf2(match: MatchModel) {
        const clone = new MatchModel(match);
        clone.scoreHref = AppState.baseUrl + match.scoreHref;
        return this.httpClient
            .post<string>(`${AppState.basePage}/scoresheet`, clone)
            .timeout(30000)
            .toPromise()
            .then(base64 => {
                this.saveBase64(base64, match.score, match);
            });
    }

    // b64toBlob(b64Data, contentType) {
    //     contentType = contentType || '';
    //     var sliceSize = 512;
    //     var byteCharacters = atob(b64Data);
    //     var byteArrays = [];

    //     for (
    //         var offset = 0;
    //         offset < byteCharacters.length;
    //         offset += sliceSize
    //     ) {
    //         var slice = byteCharacters.slice(offset, offset + sliceSize);

    //         var byteNumbers = new Array(slice.length);
    //         for (var i = 0; i < slice.length; i++) {
    //             byteNumbers[i] = slice.charCodeAt(i);
    //         }

    //         var byteArray = new Uint8Array(byteNumbers);

    //         byteArrays.push(byteArray);
    //     }

    //     var blob = new Blob(byteArrays, { type: contentType });
    //     return blob;
    // }

    public saveBase64(base64: string, name: string, match: MatchModel): Promise<string> {
        return new Promise((resolve, reject) => {
            var realData = base64.split(',')[1];
            let blob = this.b64toBlob(base64, 'application/pdf', 512);//this.b64toBlob(realData, 'application/pdf');

            let imageName = `${name}.pdf`;
            const ROOT_DIRECTORY = 'file:///sdcard//';
            const downloadFolderName = 'actionSportsTemp';
            const fileDir = ROOT_DIRECTORY + downloadFolderName + '/' + imageName;

            debugger;
            this.file
            .checkFile(ROOT_DIRECTORY + downloadFolderName, imageName).then((exists) => {
                debugger;
                this.socialSharing.shareViaWhatsApp(`${match.teamA} vs ${match.teamB} - ${match.score}`, fileDir, 'http://actionsport.spawtz.com' + match.scoreHref);
            }, err => {
                debugger
                this.file.writeFile(ROOT_DIRECTORY + downloadFolderName, imageName, blob)
                .then(() => {
                    debugger;
                    // resolve(this.pictureDir + name);
                    this.socialSharing.shareViaWhatsApp(`${match.teamA} vs ${match.teamB} - ${match.score}`, fileDir, 'http://actionsport.spawtz.com' + match.scoreHref);
                }).catch(err => {
                    console.log('error writing blob');
                    reject(err);
                });
                this.socialSharing.shareViaWhatsApp(`${match.teamA} vs ${match.teamB} - ${match.score}`, fileDir, 'http://actionsport.spawtz.com' + match.scoreHref);
            })
                
                
        });
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

    // shareImg(match: MatchModel, base64: string) {

    //     // var byteCharacters = atob(base64);
    //     // var byteNumbers = new Array(byteCharacters.length);
    //     // for (var i = 0; i < byteCharacters.length; i++) {
    //     //     byteNumbers[i] = byteCharacters.charCodeAt(i);
    //     // }

    //     // var byteArray = new Uint8Array(byteNumbers);
    //     // var blob = new Blob([byteArray], {type: 'application/pdf'});

    //     var blob = this.b64toBlob(base64, 'application/pdf', 512);
    //     var blobUrl = URL.createObjectURL(blob);
    //     debugger;
    //     let imageName = `${match.score}.pdf`;
    //     const ROOT_DIRECTORY = 'file:///sdcard//';
    //     const downloadFolderName = 'actionSportsTemp';

    //     //Create a folder in memory location
    //     this.file.createDir(ROOT_DIRECTORY, downloadFolderName, true).then(entries => {
    //             //Copy our asset/img/FreakyJolly.jpg to folder we created
    //             console.log('applicationDirectory', this.file.applicationDirectory);
    //             this.file.copyFile(
    //                     this.file.applicationDirectory + 'www/assets/img/',
    //                     imageName,
    //                     ROOT_DIRECTORY + downloadFolderName + '//',
    //                     imageName
    //                 )
    //                 .then(entries => {
    //                     //Common sharing event will open all available application to share
    //                     this.socialSharing
    //                         .share(
    //                             'Message',
    //                             'Subject',
    //                             ROOT_DIRECTORY +
    //                                 downloadFolderName +
    //                                 '/' +
    //                                 imageName,
    //                             imageName
    //                         )
    //                         .then(entries => {
    //                             console.log(
    //                                 'success ' + JSON.stringify(entries)
    //                             );
    //                         })
    //                         .catch(error => {
    //                             alert('error ' + JSON.stringify(error));
    //                         });
    //                 })
    //                 .catch(error => {
    //                     alert('error ' + JSON.stringify(error));
    //                 });
    //         })
    //         .catch(error => {
    //             alert('error ' + JSON.stringify(error));
    //         });
    // }
}
