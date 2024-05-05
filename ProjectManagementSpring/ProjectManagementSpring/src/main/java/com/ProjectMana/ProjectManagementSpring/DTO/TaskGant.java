package com.ProjectMana.ProjectManagementSpring.DTO;

import jakarta.persistence.criteria.CriteriaBuilder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
public class TaskGant {
  public Integer no ;
  public String title ;
  public String start ;
  public Long Duration ;
  public Integer status;
  public String UserName ;

  public TaskGant(Integer no, String title, String start, Long duration, Integer status, String userName) {
    this.no = no;
    this.title = title;
    this.start = start;
    Duration = duration;
    this.status = status;
    UserName = userName;
  }
}
