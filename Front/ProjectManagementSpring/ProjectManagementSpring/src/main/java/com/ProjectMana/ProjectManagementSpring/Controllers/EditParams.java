package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.enteties.ScheduleEvent;

import java.util.List;

public class EditParams {
  private String key;
  private String action;
  private List<ScheduleEvent> added;
  private List<ScheduleEvent> changed;
  private List<ScheduleEvent> deleted;
  private ScheduleEvent value;

  public String getKey() {
    return key;
  }

  public void setKey(String key) {

    this.key = key;
  }

  public String getAction() {
    return action;
  }

  public void setAction(String action) {
    this.action = action;
  }

  public List<ScheduleEvent> getAdded() {

    return added;

  }

  public void setAdded(List<ScheduleEvent> added) {
    this.added = added;
  }

  public List<ScheduleEvent> getChanged() {
    return changed;
  }

  public void setChanged(List<ScheduleEvent> changed) {
    this.changed = changed;
  }

  public List<ScheduleEvent> getDeleted() {
    return deleted;
  }

  public void setDeleted(List<ScheduleEvent> deleted) {
    this.deleted = deleted;
  }

  public ScheduleEvent getValue() {
    return value;
  }

  public void setValue(ScheduleEvent value) {
    this.value = value;
  }
}
