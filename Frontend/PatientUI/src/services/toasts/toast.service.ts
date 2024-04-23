import { Injectable, OnDestroy, TemplateRef } from '@angular/core';

/**
 * This service is used to show toasts.
 * It is a wrapper around the ngx-bootstrap toast service.
 * It is used to show toasts from anywhere in the application.
 */
@Injectable({ providedIn: 'root' })
export class ToastService implements OnDestroy {
  toasts: any[] = [];

  private show(header: string, textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ header, textOrTpl, ...options });
  }
  private showNoHeader(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast: any) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }

  showSuccess(header: string, textOrTpl: string | TemplateRef<any>) {
    this.show(header, textOrTpl, { classname: 'bg-success text-light', delay: 5000 });
  }

  showSuccessNoHeader(textOrTpl: string | TemplateRef<any>) {
    this.showNoHeader(textOrTpl, { classname: 'bg-success text-light', delay: 5000 });
  }

  showError(header: string, textOrTpl: string | TemplateRef<any>) {
    this.show(header, textOrTpl, { classname: 'bg-danger text-light', delay: 6000 });
  }

  showErrorNoHeader(textOrTpl: string | TemplateRef<any>) {
    this.showNoHeader(textOrTpl, { classname: 'bg-danger text-light', delay: 6000 });
  }

  showWarning(header: string, textOrTpl: string | TemplateRef<any>) {
    this.show(header, textOrTpl, { classname: 'bg-warning text-dark', delay: 5000 });
  }

  showWarningNoHeader(textOrTpl: string | TemplateRef<any>) {
    this.showNoHeader(textOrTpl, { classname: 'bg-warning text-dark', delay: 5000 });
  }

  showInfo(header: string, textOrTpl: string | TemplateRef<any>) {
    this.show(header, textOrTpl, { classname: 'bg-info text-dark', delay: 5000 });
  }

  showInfoNoHeader(textOrTpl: string | TemplateRef<any>) {
    this.showNoHeader(textOrTpl, { classname: 'bg-info text-dark', delay: 5000 });
  }

  ngOnDestroy(): void {
    this.clear();
  }
}
