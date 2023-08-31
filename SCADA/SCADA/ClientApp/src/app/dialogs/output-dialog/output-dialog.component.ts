import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';
import { TagDTO } from 'src/app/model/TagDTO';
import {MatSnackBar} from '@angular/material/snack-bar';
import { TagserviceService } from 'src/app/services/TagService/tagservice.service';

@Component({
  selector: 'app-output-dialog',
  templateUrl: './output-dialog.component.html',
  styleUrls: ['./output-dialog.component.css']
})
export class OutputDialogComponent {
  outputForm = new FormGroup({
    outputType: new FormControl('', [Validators.required, Validators.minLength(2)]),
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required, Validators.minLength(5)]),
    ioAddress: new FormControl('', [Validators.required, Validators.minLength(1)]),
    initialValue: new FormControl(null, [Validators.required, Validators.minLength(1)]),
    lowLimit: new FormControl(null, [Validators.required, Validators.minLength(1)]),
    highLimit: new FormControl(null, [Validators.required, Validators.minLength(1)]),
    unit: new FormControl('', [Validators.required, Validators.minLength(1)])

  });

  constructor(private snackBar: MatSnackBar, public dialogRef: MatDialogRef<OutputDialogComponent>, public tagService: TagserviceService) {}

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

  addTag() {
    let tag: TagDTO = {
      type: this.outputForm.value.outputType === 'DIGITAL' ? 'Digital Output' : "Analog Output",
      id: null,
      name: this.outputForm.value.name as string,
      ioAddress: this.outputForm.value.ioAddress as string,
      description: this.outputForm.value.description as string,
      value: 0,
      driver: '',
      scanTime: 0,
      isScanOn: this.outputForm.get('isScanOn')?.value ?? false,
      lowLimit: this.outputForm.value.lowLimit ?? 0,
      highLimit: this.outputForm.value.highLimit ?? 0,
      units: this.outputForm.value.unit ?? '',
      initialValue: this.outputForm.value.initialValue ?? 0
    }

    console.log(tag);

    this.tagService.addInputTag(tag).subscribe({
      next: (res) => {
        this.snackBar.open('Added tag successfully!', "", {duration: 2000});
        this.dialogRef.close();
      }
    })
  }
}
