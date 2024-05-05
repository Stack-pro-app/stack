package com.ProjectMana.ProjectManagementSpring.DTO;

import jakarta.persistence.criteria.CriteriaBuilder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
public class taskDTO {
    public Integer no ;
    public String title ;
    public String description ;
    public Integer projectId;
    public Integer userId;
  public String start ;
    public String end ;
    public Integer status;

  public taskDTO(Integer no, String title, String description, Integer projectId, Integer userId, String start, String end, Integer status) {
    this.no = no;
    this.title = title;
    this.description = description;
    this.projectId = projectId;
    this.userId = userId;
    this.start = start;
    this.end = end;
    this.status = status;
  }

  public taskDTO(String title, String description, Integer projectId, Integer userId, String start, String end, Integer status) {
    this.title = title;
    this.description = description;
    this.projectId = projectId;
    this.userId = userId;
    this.start = start;
    this.end = end;
    this.status = status;
  }
}
