package com.ProjectMana.ProjectManagementSpring.DTO;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;
@Data
@NoArgsConstructor
public class GanttDTO {
  public Integer id ;

  public String  projectName ;
  public String  start;
  public  String  end;
  public  List<TaskGant> tasks ;

  public GanttDTO(Integer id, String projectName, String start, String end, List<TaskGant> tasks) {
    this.id = id;
    this.projectName = projectName;
    this.start = start;
    this.end = end;
    this.tasks = tasks;
  }

  public GanttDTO(Integer id, String projectName, String start, String end) {
    this.id = id;
    this.projectName = projectName;
    this.start = start;
    this.end = end;
  }
}
