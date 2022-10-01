import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user:any = {};
  loggedIn:boolean;

  constructor(private accountservice:AccountService) { }


  login(){
    this.accountservice.login(this.user).subscribe(x=>{
      console.log(x);
      this.loggedIn = true;
    },error=>{
      console.log(error);
    });
    console.log(this.user); 
  }

  logout(){
    this.loggedIn = false;
  }

  ngOnInit(): void {
  }

}
