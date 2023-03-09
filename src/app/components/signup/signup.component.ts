import { Component , OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/Helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit{
  type : string = "password";
 isText : boolean = false;
 eyeIcon : string = "fa-eye-slash";
  signUpForm! : FormGroup;
  constructor(private fb : FormBuilder, private auth: AuthService, private router : Router){}
  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      Firstname: ['',Validators.required],
      Lastname: ['',Validators.required],
      username: ['',Validators.required],
      Email: ['',Validators.required],
      password: ['',Validators.required],


    })
  }


 hideShowBtn(){
  this.isText = !this.isText
  this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash"
  this.isText ? this.type = "text" : this.type = "password"
 };

 onsignup(){
  if(this.signUpForm.valid){
    console.log(this.signUpForm.value);
    this.auth.signup(this.signUpForm.value).subscribe({
      next : (res=>{
          alert(res.message)
          this.signUpForm.reset();
          this.router.navigate(['login'])
      }),
      error : (err=>{
        alert(err.error.message);
      })
      
    })
  }
  else{
    ValidateForm.validateAllFormFields(this.signUpForm)
  }
 }
 
}
