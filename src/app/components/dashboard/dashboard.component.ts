import { Component, OnInit } from '@angular/core';
import { Users } from 'src/app/models/users';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  userarray : Users[] =[];
  ngOnInit(): void {
   this.getUser()
  }

  getUser(){
    this.authserve.GetUser().subscribe(res =>{
    console.log(res);
    this.userarray = res
    })
  }

  deleteUser(id:string){
    this.authserve.DeleteUser(id).subscribe(response =>{
      console.log(response)
      this.getUser()
    })
  }

  

  constructor(private authserve : AuthService){}

}
