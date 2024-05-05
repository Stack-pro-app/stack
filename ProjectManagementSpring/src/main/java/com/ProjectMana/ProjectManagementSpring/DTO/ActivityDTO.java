package com.ProjectMana.ProjectManagementSpring.DTO;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
@Data
@NoArgsConstructor
public class ActivityDTO {
  private Integer id;

  private LocalDateTime date;
  private Integer status ;
  private String UserName;
 private String TaskName;

  public ActivityDTO(Integer id, LocalDateTime date, Integer status, String userName, String taskName) {
    this.id = id;
    this.date = date;
    this.status = status;
    UserName = userName;
    TaskName = taskName;
  }
}
