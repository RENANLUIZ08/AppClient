import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss'],
})
export class TitleComponent implements OnInit {
  @Input() title?: string;
  @Input() iconName: string = 'fa fa-user';
  @Input() subtitle?: string;
  @Input() showList?: boolean = true;

  listar(): void {
    this.router.navigate([`/${this.title?.toLocaleLowerCase()}/list`]);
  }
  constructor(private router: Router) {}

  ngOnInit(): void {}
}
