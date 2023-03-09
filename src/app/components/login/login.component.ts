import { Component , OnInit} from '@angular/core';
import {FormGroup,FormBuilder,Validators, FormControl} from '@angular/forms'
import { Router } from '@angular/router';
import ValidateForm from 'src/app/Helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
 type : string = "password"
 isText : boolean = false
 eyeIcon : string = "fa-eye-slash"
 loginForm! : FormGroup ;
 constructor(private fb:FormBuilder ,private auth:AuthService, private router : Router){}
 ngOnInit():void{
  this.loginForm = this.fb.group({
    username : ['',Validators.required],
    password : ['', Validators.required]

  })
 }
 hideShowBtn(){
  this.isText = !this.isText
  this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash"
  this.isText ? this.type = "text" : this.type = "password"
 }

 onSubmit(){
  if(this.loginForm.valid){
    console.log(this.loginForm.value);
    //send to db  
    this.auth.login(this.loginForm.value).subscribe({
      next:(res)=>{
        alert(res.message)
        this.loginForm.reset();
        this.router.navigate(['dashboard']);
      },
      error:(err)=>{
        alert(err.error.message)
      }
    })
  }
  else{
    ValidateForm.validateAllFormFields(this.loginForm)

    alert("form is invalid")
    
  }
 }

 
  
}
