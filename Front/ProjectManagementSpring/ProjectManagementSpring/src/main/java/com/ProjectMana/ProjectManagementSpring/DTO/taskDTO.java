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

    public taskDTO(Integer no, String title, String description, Integer projectId, Integer userId, String end ,String start) {
        this.no = no;
        this.title = title;
        this.description = description;
        this.projectId = projectId;
        this.userId = userId;
        this.end = end;
        this.start=start;
    }

    public taskDTO(String title, String description, Integer projectId, Integer userId, String end,String start) {
        this.title = title;
        this.description = description;
        this.projectId = projectId;
        this.userId = userId;
        this.end = end;
        this.start=start;
    }

}