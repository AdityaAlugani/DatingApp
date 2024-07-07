import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventPageUnsavedGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  if(component.EditForm?.dirty==true)
  return confirm("Are you sure? any unsaved changes will be lost!!");

  return true;
};
