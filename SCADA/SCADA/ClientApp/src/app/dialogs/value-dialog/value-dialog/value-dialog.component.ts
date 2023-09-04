import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValueDTO } from 'src/app/model/valueDTO';
import { TagserviceService } from 'src/app/services/TagService/tagservice.service';

@Component({
  selector: 'app-value-dialog',
  templateUrl: './value-dialog.component.html',
  styleUrls: ['./value-dialog.component.css']
})
export class ValueDialogComponent {
  valueForm = new FormGroup({
    value: new FormControl(0, [Validators.required, Validators.minLength(1)])
  })
  
  constructor(private snackBar: MatSnackBar, @Inject(MAT_DIALOG_DATA) public data: string, public dialogRef: MatDialogRef<ValueDialogComponent>, public tagService: TagserviceService) {}

  closeDialog() {
    this.dialogRef.close();
  }

  isFormValid(): boolean {
    return this.valueForm.valid;
  }

  changeValue() {
    let valueDTO: ValueDTO = {
      IOAddress: this.data,
      Value: this.valueForm.value.value ?? 0
    }

    console.log(valueDTO);
    this.tagService.addOutputValue(valueDTO).subscribe({
      next: (res) => {
        this.snackBar.open('Added tag successfully!', "", {duration: 2000});
        this.dialogRef.close();
      }
    })
  }

}
