import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento/Evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers: [EventoService]
})
export class EventosComponent implements OnInit {
  //
  modalRef?: BsModalRef;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) {}

  public ngOnInit(): void {
    this.getEventos();
  }

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public esconderImagens: boolean = true;

  public widthImg: number = 100;
  public marginImg: number = 2;

  private _filtroLista: string = '';

  public get filtroLista() {
    return this._filtroLista;
  }
  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this.filtroLista)
      : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public getEventos(): void {
    const observer = {
      next: (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any) => console.log(error),
      complete: () => {},
    };

    this.eventoService.getEventos().subscribe(observer);
    // (eventos: Evento[]) => {
    //   this.eventos = eventos;
    //   this.eventosFiltrados = this.eventos;
    // },
    // (error) => console.log(error)
  }
  public EsconderImagens(): void {
    this.esconderImagens = !this.esconderImagens;
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.toastr.success('Evento excluido com sucesso', 'Excluido!');
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
