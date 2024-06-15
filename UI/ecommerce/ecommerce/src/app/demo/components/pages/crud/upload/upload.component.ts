import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-upload',
    standalone: true,
    imports: [],
    templateUrl: './upload.component.html',
    styleUrl: './upload.component.scss',
})
export class UploadComponent {
    message: string;
    progress: number;

    @Output() onUploadFinished = new EventEmitter();
    @Input() fileName;

    constructor(private http: HttpClient) {}

    public uploadFile = (files) => {
        if (files.length == 0) return;

        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        formData.append('fileName', this.fileName);

        this.http
            .post('https://localhost:7088/api/Images/Upload', formData, {
                reportProgress: true,
                observe: 'events',
            })
            .subscribe({
                next: (event) => {
                    if (event.type === HttpEventType.UploadProgress) {
                        this.progress = Math.round(
                            (100 * event.loaded) / event.total
                        );
                    } else if (event.type === HttpEventType.Response) {
                        this.message = 'Upload Success';
                        this.onUploadFinished.emit(event.body);
                    }
                },
            });
    };
}
