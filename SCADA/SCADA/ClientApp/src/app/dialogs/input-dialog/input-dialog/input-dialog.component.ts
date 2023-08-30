import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { TagserviceService } from 'src/app/services/TagService/tagservice.service';

@Component({
  selector: 'app-input-dialog',
  templateUrl: './input-dialog.component.html',
  styleUrls: ['./input-dialog.component.css']
})
export class InputDialogComponent {
  outputForm = new FormGroup({
    outputType: new FormControl('', [Validators.required, Validators.minLength(2)]),
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required, Validators.minLength(5)]),
    ioAddress: new FormControl('', [Validators.required, Validators.minLength(1)]),
    driver: new FormControl('', [Validators.required, Validators.minLength(1)]),
    scanTime: new FormControl('', [Validators.required]),
    isScanOn: new FormControl('', [Validators.required]),
    lowLimit: new FormControl('', [Validators.required]),
    highLimit: new FormControl('', [Validators.required]),
    unit: new FormControl('', [Validators.required, Validators.minLength(1)])

  });

  constructor(private sanitizer: DomSanitizer, public dialogRef: MatDialogRef<InputDialogComponent>, public tagService: TagserviceService) {}

  ngOnInit() : void {

    this.outputForm.get('outputType')?.valueChanges.subscribe(outputType => {
      if (outputType === 'DIGITAL') {
        this.outputForm.get('unit')?.disable(); 
        this.outputForm.get('lowLimit')?.disable();
        this.outputForm.get('highLimit')?.disable();
      } else {
        this.outputForm.get('unit')?.enable(); 
        this.outputForm.get('lowLimit')?.enable();
        this.outputForm.get('highLimit')?.enable();
      }
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  isFormValid(): boolean {
    return this.outputForm.valid;
  }
}
