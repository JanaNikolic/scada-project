import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { TagDTO } from 'src/app/model/TagDTO';
import { TagserviceService } from 'src/app/services/TagService/tagservice.service';

@Component({
  selector: 'app-input-dialog',
  templateUrl: './input-dialog.component.html',
  styleUrls: ['./input-dialog.component.css']
})
export class InputDialogComponent {
  inputForm = new FormGroup({
    outputType: new FormControl('', [Validators.required, Validators.minLength(2)]),
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required, Validators.minLength(5)]),
    ioAddress: new FormControl('', [Validators.required, Validators.minLength(1)]),
    driver: new FormControl('', [Validators.required, Validators.minLength(1)]),
    scanTime: new FormControl(0, [Validators.required]),
    isScanOn: new FormControl(false, [Validators.required]),
    lowLimit: new FormControl(0, [Validators.required]),
    highLimit: new FormControl(0, [Validators.required]),
    unit: new FormControl('', [Validators.required, Validators.minLength(1)])

  });

  constructor(private sanitizer: DomSanitizer, private snackBar: MatSnackBar, public dialogRef: MatDialogRef<InputDialogComponent>, public tagService: TagserviceService) {}

  ngOnInit() : void {

    this.inputForm.get('outputType')?.valueChanges.subscribe(outputType => {
      if (outputType === 'DIGITAL') {
        this.inputForm.get('unit')?.disable(); 
        this.inputForm.get('lowLimit')?.disable();
        this.inputForm.get('highLimit')?.disable();
      } else {
        this.inputForm.get('unit')?.enable(); 
        this.inputForm.get('lowLimit')?.enable();
        this.inputForm.get('highLimit')?.enable();
      }
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  addTag() {
    let tag: TagDTO = {
      type: this.inputForm.value.outputType === 'DIGITAL' ? 'Digital Input' : "Analog Input",
      id: null,
      name: this.inputForm.value.name as string,
      ioAddress: this.inputForm.value.ioAddress as string,
      description: this.inputForm.value.description as string,
      value: 0,
      driver: this.inputForm.value.driver as string,
      scanTime: this.inputForm.value.scanTime ?? 0,
      isScanOn: this.inputForm.get('isScanOn')?.value ?? false,
      lowLimit: this.inputForm.value.lowLimit ?? 0,
      highLimit: this.inputForm.value.highLimit ?? 0,
      units: this.inputForm.value.unit ?? '',
      initialValue: 0
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
