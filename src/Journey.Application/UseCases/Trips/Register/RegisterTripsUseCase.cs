﻿using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.Register;
public class RegisterTripsUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var entity = new Trip
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        dbContext.Trips.Add(entity);

        dbContext.SaveChanges();

        return new ResponseShortTripJson
        {
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Name = entity.Name,
            Id = entity.Id
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        if ( string.IsNullOrWhiteSpace(request.Name)) 
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.NAME_EMPTY);
        }

        if(request.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
        }

        if (request.EndDate.Date < request.StartDate.Date)
        {
            throw new ErrorOnValidationException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
