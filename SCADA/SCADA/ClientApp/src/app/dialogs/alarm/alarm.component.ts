import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DomSanitizer} from "@angular/platform-browser";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {TagserviceService} from "../../services/TagService/tagservice.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AlarmDTO} from "../../model/Alarm";

@Component({
  selector: 'app-alarm',
  templateUrl: './alarm.component.html',
  styleUrls: ['./alarm.component.css']
})
export class AlarmComponent{
  constructor(private snackBar: MatSnackBar, public dialogRef: MatDialogRef<AlarmComponent>, public tagService: TagserviceService, @Inject(MAT_DIALOG_DATA) public data: any) {
    console.log(this.data);
  }

  inputForm = new FormGroup({
    threshold: new FormControl(0, [Validators.required, Validators.min(this.data.lowLimit), Validators.max(this.data.highLimit)]),
    type: new FormControl('ABOVE', [Validators.required]),
    priority: new FormControl('Level1', [Validators.required, Validators.minLength(5)])
  });

  closeDialog() {
    this.dialogRef.close();
  }

  isFormValid(): boolean {
    return this.inputForm.valid;
  }

  addAlarm() {
    let body: AlarmDTO = {
      threshold: this.inputForm.value.threshold ?? 0,
      type: this.inputForm.value.type as string,
      priority: this.inputForm.value.priority as string,
      tagId: this.data.id ?? 0};
    console.log(body);

    this.tagService.addAlarm(body).subscribe({
      next: (res) => {
        this.snackBar.open('Added alarm successfully!', "", {duration: 2000});
        this.dialogRef.close();
      }
    })
  }
}
