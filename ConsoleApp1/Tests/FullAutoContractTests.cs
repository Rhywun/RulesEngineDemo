﻿using ConsoleApp1.Models;
using System;

namespace ConsoleApp1.Tests
{
    static public class FullAutoContractTests
    {
        static public FullAutoContract Contract1 = new FullAutoContract()
        {
            ClientName = "TESTCLIENT",
            DealerPolicyNumber = "3333",
            DealerPolicyNumberSuffix = "A",
            ProductKey = "1",
            WarrantyKey = "1",
            ProducerCode = "TESTDLR1",
            ProgramName = "VSCProgram1",
            InsuranceType = "FD",
            CustomerContractNumber = "3333-A",
            ContractStatus = "A",
            WarrantySKU = "WARRANTYSKU3",
            WarrantyDescription = "Vehicle Service Contract 24/24",
            WarrantyType = "VSC",
            WarrantySubType = "VSC",
            CoverageGroup = "VehicleServiceContract",
            ProductType = "Auto",
            ProductSubType = "Car",
            TransactionDate = DateTime.Parse("2018-12-31"),
            ContractPurchaseDate = DateTime.Parse("2018-12-31"),
            RenewalDate = DateTime.Parse("2018-12-31"),
            ProductPurchaseDate = DateTime.Parse("2018-12-31"),
            VehicleInServiceDate = DateTime.Parse("2018-12-31"),
            TermMileage = 24000,
            TermMonths = 24,
            TermHours = 0,
            ManufacturerTermMonths = 36,
            StartOfRiskType = "EOMW",
            ManufacturerEffectiveDate = DateTime.Parse("2018-12-31"),
            ManufacturerExpirationDate = DateTime.Parse("2021-12-30"),
            CoverageEffectiveDate = DateTime.Parse("2021-12-31"),
            CoverageExpirationDate = DateTime.Parse("2023-12-31"),
            MileageAtPurchase = 100,
            MileageAtCoverageStart = 100,
            HoursAtPurchase = 0,
            Make = "Test Mfg",
            Model = "Test Model",
            ModelYear = 2018,
            RateClass = "3",
            New_Used = "New",
            VIN = "123456ABCDEFGHIJK",
            CC = "1000",
            ProductPurchasePrice = 22000.00m,
            LoanOrLease = "Loan",
            LoanToValue = 0.91m,
            NADA_MSRPValue = 24000.00m,
            AmountFinanced = 20000.00m,
            LoanAPR = 0.049m,
            AgreementType = "Voluntary",
            UseType = "Personal",
            SalesChannel = "AM",
            ClaimLimitOfLiability = 250.00m,
            ContractLimitOfLiability = 500.00m,
            ClaimCountLimit = 2,
            DeductibleType = "STANDARD",
            Deductible = 100.00m,
            DeductibleAtProducer = 100.00m,
            RetailAmount = 1000.00m,
            AdminAmount = 0.00m,
            Commission = 0.00m,
            RemitToAmTrust = 503.00m,
            InsurancePremium = 500.00m,
            ReserveAmount = 425.00m,
            Ceding_Risk_Fee = 72.50m,
            ContingencyFee = 1.00m,
            PremiumTax = 12.50m,
            BaseReserveAmount = 425.00m,
            SurchargeReserveAmount = 0.00m,
            SurchargePremium = 0.00m,
            ServiceFee = 0.00m,
            BrokerFee = 0.00m,
            ObligorFee = 2.00m,
            NCFCManagementFee = 0.00m,
            RiskPoolFee = 0.00m,
            FederalExciseTax = 0.00m,
            CancellationDate = null,
            CancellationHours = null,
            CancellationMileage = null,
            CancellationPercentageDecimal = null,
            CancellationReason = null,
            Currency = "USD",
            ReInsuranceCompanyCode = "ACME",
            ReInsuranceCompanyName = "ACME REINSURANCE",
            ReinsuranceType = "CFC",
            Administrator = "Administrator",
            InsuranceCompanyName = "Wesco",
            ObligorCode = "WESCO",
            ObligorState = "FL",
            ObligorCountry = "USA",
            FormNumber = "124.44.5C",
            CompanyNameOfPurchaser = null,
            CustomerFirstName = "John",
            CustomerLastName = "Doe",
            CustomerAddress = "Test Address",
            CustomerAddress2 = "Apartment 2",
            CustomerCity = "Cleveland",
            CustomerState = "OH",
            CustomerZip = "44122",
            CustomerCountry = "USA",
            UserDefinedDecimal1 = 15.00m,
            UserDefinedDecimal2 = 20.00m,
            UserDefinedDecimal3 = 20.00m,
            UserDefinedString1 = "Test",
            UserDefinedDate1 = DateTime.Parse("2019-01-02"),
        };
    }
}
