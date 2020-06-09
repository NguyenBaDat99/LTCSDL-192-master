import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as $ from 'jquery';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  dsDonHang: any = {};

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
  }

  ngOnInit() {
  }

  submit(){
    var begin = $('#beginDate').val();
    var end = $('#endDate').val();
    var para: any = {
      begin: begin,
      end: end
    }    
    this.http.post('https://localhost:44377/api/De2/proc_DanhSachDHTrongKhoang', para).subscribe(result => {
      var res: any = result;
      this.dsDonHang = res.data;
    }, error => console.error(error));
  }

}
